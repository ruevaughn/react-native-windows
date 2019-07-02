import React, { Component } from 'react'
import PropTypes from 'prop-types'
import styles from './styles'
import {
  Text,
  View,
  TouchableOpacity,
} from 'react-native';

export default class MainPage extends Component {
  static propTypes = {
    isFocusable: PropTypes.bool
  }

  constructor(props) {
    super(props)
  }
  render() {
    return (
      <View isFocusable={this.props.isFocusable} accessibilityLabel={'Accessibility Page'} style={styles.content}>
        <Text selectable={this.props.isFocusable} accessibilityLable={'Accessibility Page'} style={styles.title}>Accessibility Page</Text>
        <View style={styles.content}>
          <Text accessibilityLabel={'Different UI Elements'} selectable={this.props.isFocusable}>Different UI Elements</Text>
          <View isFocusable={this.props.isFocusable} accessibilityLabel={"Touchable opacity"}>
          <TouchableOpacity>
            <Text>Click here</Text>
          </TouchableOpacity>
          </View>

        </View>
      </View>
    )
  }

}
