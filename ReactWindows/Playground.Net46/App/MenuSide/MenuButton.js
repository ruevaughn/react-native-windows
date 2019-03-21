import React, { Component } from 'react'
import PropTypes from 'prop-types'
import {
  Button,
  Text,
  View,
} from 'react-native';
import styles from './styles'

export default class MenuButton extends Component {
  static propTypes = {
    caption: PropTypes.string,
    onClick: PropTypes.func.isRequired
  }

  render() {
    return (
      <View style={styles.button}>
        {/* <Text> {this.props.caption}</Text> */}
        <Button
          onPress = {this.props.onClick} 
          title={this.props.caption}
          />
      </View>
    )
  }
}