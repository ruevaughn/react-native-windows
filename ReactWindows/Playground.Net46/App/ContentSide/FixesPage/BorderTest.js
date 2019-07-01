import React, { Component } from 'react'
import PropTypes from 'prop-types'
import {
  Text,
  View,
} from 'react-native'
import styles from './styles'

export default class BorderTest extends Component {
  static propTypes = {
    isFocusable: PropTypes.bool
  }

  render() {
    return (
      <View style={styles.content}>
        <View style={styles.testBar}>
          <View style={styles.item}>
            <Text selectable={this.props.isFocusable} accessibilityLabel={'Border left and right width does work'} style={styles.subCaption}>Border left and right width does work</Text>
            <View style={styles.borderWidth} />
          </View>
          <View style={styles.item}>
            <Text selectable={this.props.isFocusable} accessibilityLabel={'Border top\\left\\bottom\\right color does not work'} style={styles.subCaption}>Border top\left\bottom\right color does not work</Text>
            <View style={styles.borderColor} />
          </View>
          <View style={styles.item}>
            <Text selectable={this.props.isFocusable} accessibilityLabel={'Border Start|End Width does not work'} style={styles.subCaption}>Border Start|End Width does not work</Text>
            <View style={styles.borderStartWidth}> 
              <Text>Hello</Text>
            </View>
          </View>
        </View>
      </View>
    )
  }
}
