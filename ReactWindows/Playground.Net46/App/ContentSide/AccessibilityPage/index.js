import React, { Component } from 'react'
import PropTypes from 'prop-types'
import styles from './styles'
import {
  Text,
  View,
  TouchableOpacity,
} from 'react-native';

const createFocusableComponent = require('FocusableWindows');
const FocusableTouchableOpacity = createFocusableComponent(TouchableOpacity);

export default class MainPage extends Component {
  static propTypes = {
    isFocusable: PropTypes.bool
  }
  
  componentDidMount() {
	  if(this.refs.focusableButton){
		  this.refs.focusableButton.focus()
	  }
  }

  constructor(props) {
    super(props)
  }
  render() {
    return (
      <View isFocusable={this.props.isFocusable} accessibilityLabel={'Accessibility Page'} style={styles.content}>
        <Text selectable={this.props.isFocusable} accessibilityLable={'Accessibility Title'} style={styles.title}>Accessibility Page</Text>
        <View style={styles.content}>
          <Text accessibilityLabel={'Different UI Elements'} selectable={this.props.isFocusable}>Different UI Elements</Text>
          <View>
            <FocusableTouchableOpacity ref='focusableButton' controlTypeName='button' isTabStop={true} accessibilityLabel='Focusable Touchable Opacity'>
              <Text>Focusable Touchable Opacity button</Text>
            </FocusableTouchableOpacity>
            <FocusableTouchableOpacity controlTypeName='hyperlink' isTabStop={true} accessibilityLabel='Focusable Touchable Opacity'>
              <Text>Focusable Touchable Opacity link</Text>
            </FocusableTouchableOpacity>
            <FocusableTouchableOpacity controlTypeName='radioButton' isTabStop={true} accessibilityLabel='Focusable Touchable Opacity'>
              <Text>Focusable Touchable Opacity radiobutton</Text>
            </FocusableTouchableOpacity>
          </View>
        </View>
      </View>
    )
  }

}
